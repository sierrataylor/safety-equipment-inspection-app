import { Component, OnInit } from '@angular/core';
@Component({
	selector: 'app-scanner',
	templateUrl: './qrgenerator.component.html',
	styleUrls: ['./qrgenerator.component.css']
})
"use strict";


namespace app {

	function initialize(): void {
		getElem("loading").style.display = "none";
		getElem("loaded").style.removeProperty("display");
		let elems = document.querySelectorAll("input[type=number], input[type=text], textarea");
		for (let el of elems) {
			if (el.id.indexOf("version-") != 0)
				(el as any).oninput = redrawQrCode;
		}
		elems = document.querySelectorAll("input[type=radio], input[type=checkbox]");
		for (let el of elems)
			(el as HTMLInputElement).onchange = redrawQrCode;
		redrawQrCode();
	}


	function redrawQrCode(): void {
		// Show/hide rows based on bitmap/vector image output
		const bitmapOutput: boolean = getInput("output-format-bitmap").checked;
		const scaleRow: HTMLElement = getElem("scale-row");
		const svgXmlRow: HTMLElement = getElem("svg-xml-row");
		if (bitmapOutput) {
			scaleRow.style.removeProperty("display");
			svgXmlRow.style.display = "none";
		} else {
			scaleRow.style.display = "none";
			svgXmlRow.style.removeProperty("display");
		}
		const svgXml = getElem("svg-xml-output") as HTMLTextAreaElement;
		svgXml.value = "";

		// Reset output images in case of early termination
		const canvas = getElem("qrcode-canvas") as HTMLCanvasElement;
		const svg = (document.getElementById("qrcode-svg") as Element) as SVGElement;
		canvas.style.display = "none";
		svg.style.display = "none";

		// Returns a QrCode.Ecc object based on the radio buttons in the HTML form.
		function getInputErrorCorrectionLevel(): qrcodegen.QrCode.Ecc {
			if (getInput("errcorlvl-medium").checked)
				return qrcodegen.QrCode.Ecc.MEDIUM;
			else if (getInput("errcorlvl-quartile").checked)
				return qrcodegen.QrCode.Ecc.QUARTILE;
			else if (getInput("errcorlvl-high").checked)
				return qrcodegen.QrCode.Ecc.HIGH;
			else  // In case no radio button is depressed
				return qrcodegen.QrCode.Ecc.LOW;
		}

		// Get form inputs and compute QR Code
		const ecl: qrcodegen.QrCode.Ecc = getInputErrorCorrectionLevel();
		const text: string = (getElem("text-input") as HTMLTextAreaElement).value;
		const segs: Array<qrcodegen.QrSegment> = qrcodegen.QrSegment.makeSegments(text);
		const minVer: number = parseInt(getInput("version-min-input").value, 10);
		const maxVer: number = parseInt(getInput("version-max-input").value, 10);
		const mask: number = parseInt(getInput("mask-input").value, 10);
		const boostEcc: boolean = getInput("boost-ecc-input").checked;
		const qr: qrcodegen.QrCode = qrcodegen.QrCode.encodeSegments(segs, ecl, minVer, maxVer, mask, boostEcc);

		// Draw image output
		const border: number = parseInt(getInput("border-input").value, 10);
		const lightColor: string = getInput("light-color-input").value;
		const darkColor: string = getInput("dark-color-input").value;
		if (border < 0 || border > 100)
			return;
		if (bitmapOutput) {
			const scale: number = parseInt(getInput("scale-input").value, 10);
			if (scale <= 0 || scale > 30)
				return;
			drawCanvas(qr, scale, border, lightColor, darkColor, canvas);
			canvas.style.removeProperty("display");
		} else {
			const code: string = toSvgString(qr, border, lightColor, darkColor);
			const viewBox: string = (/ viewBox="([^"]*)"/.exec(code) as RegExpExecArray)[1];
			const pathD: string = (/ d="([^"]*)"/.exec(code) as RegExpExecArray)[1];
			svg.setAttribute("viewBox", viewBox);
			(svg.querySelector("path") as Element).setAttribute("d", pathD);
			(svg.querySelector("rect") as Element).setAttribute("fill", lightColor);
			(svg.querySelector("path") as Element).setAttribute("fill", darkColor);
			svg.style.removeProperty("display");
			svgXml.value = code;
		}

		// Returns a string to describe the given list of segments.
		function describeSegments(segs: Array<qrcodegen.QrSegment>): string {
			if (segs.length == 0)
				return "none";
			else if (segs.length == 1) {
				const mode: qrcodegen.QrSegment.Mode = segs[0].mode;
				const Mode = qrcodegen.QrSegment.Mode;
				if (mode == Mode.NUMERIC) return "numeric";
				if (mode == Mode.ALPHANUMERIC) return "alphanumeric";
				if (mode == Mode.BYTE) return "byte";
				if (mode == Mode.KANJI) return "kanji";
				return "unknown";
			} else
				return "multiple";
		}

		// Returns the number of Unicode code points in the given UTF-16 string.
		function countUnicodeChars(str: string): number {
			let result: number = 0;
			for (const ch of str) {
				const cc = ch.codePointAt(0) as number;
				if (0xD800 <= cc && cc < 0xE000)
					throw new RangeError("Invalid UTF-16 string");
				result++;
			}
			return result;
		}

		// Show the QR Code symbol's statistics as a string
		getElem("statistics-output").textContent = `QR Code version = ${qr.version}, ` +
			`mask pattern = ${qr.mask}, ` +
			`character count = ${countUnicodeChars(text)},\n` +
			`encoding mode = ${describeSegments(segs)}, ` +
			`error correction = level ${"LMQH".charAt(qr.errorCorrectionLevel.ordinal)}, ` +
			`data bits = ${qrcodegen.QrSegment.getTotalBits(segs, qr.version) as number}.`;
	}


	// Draws the given QR Code, with the given module scale and border modules, onto the given HTML
	// canvas element. The canvas's width and height is resized to (qr.size + border * 2) * scale.
	// The drawn image is purely dark and light, and fully opaque.
	// The scale must be a positive integer and the border must be a non-negative integer.
	function drawCanvas(qr: qrcodegen.QrCode, scale: number, border: number, lightColor: string, darkColor: string, canvas: HTMLCanvasElement): void {
		if (scale <= 0 || border < 0)
			throw new RangeError("Value out of range");
		const width: number = (qr.size + border * 2) * scale;
		canvas.width = width;
		canvas.height = width;
		let ctx = canvas.getContext("2d") as CanvasRenderingContext2D;
		for (let y = -border; y < qr.size + border; y++) {
			for (let x = -border; x < qr.size + border; x++) {
				ctx.fillStyle = qr.getModule(x, y) ? darkColor : lightColor;
				ctx.fillRect((x + border) * scale, (y + border) * scale, scale, scale);
			}
		}
	}


	// Returns a string of SVG code for an image depicting the given QR Code, with the given number
	// of border modules. The string always uses Unix newlines (\n), regardless of the platform.
	function toSvgString(qr: qrcodegen.QrCode, border: number, lightColor: string, darkColor: string): string {
		if (border < 0)
			throw new RangeError("Border must be non-negative");
		let parts: Array<string> = [];
		for (let y = 0; y < qr.size; y++) {
			for (let x = 0; x < qr.size; x++) {
				if (qr.getModule(x, y))
					parts.push(`M${x + border},${y + border}h1v1h-1z`);
			}
		}
		return `<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">
<svg xmlns="http://www.w3.org/2000/svg" version="1.1" viewBox="0 0 ${qr.size + border * 2} ${qr.size + border * 2}" stroke="none">
	<rect width="100%" height="100%" fill="${lightColor}"/>
	<path d="${parts.join(" ")}" fill="${darkColor}"/>
</svg>
`
	}


	export function handleVersionMinMax(which: "min" | "max"): void {
		const minElem: HTMLInputElement = getInput("version-min-input");
		const maxElem: HTMLInputElement = getInput("version-max-input");
		let minVal: number = parseInt(minElem.value, 10);
		let maxVal: number = parseInt(maxElem.value, 10);
		minVal = Math.max(Math.min(minVal, qrcodegen.QrCode.MAX_VERSION), qrcodegen.QrCode.MIN_VERSION);
		maxVal = Math.max(Math.min(maxVal, qrcodegen.QrCode.MAX_VERSION), qrcodegen.QrCode.MIN_VERSION);
		if (which == "min" && minVal > maxVal)
			maxVal = minVal;
		else if (which == "max" && maxVal < minVal)
			minVal = maxVal;
		minElem.value = minVal.toString();
		maxElem.value = maxVal.toString();
		redrawQrCode();
	}


	function getElem(id: string): HTMLElement {
		const result: HTMLElement | null = document.getElementById(id);
		if (result instanceof HTMLElement)
			return result;
		throw new Error("Assertion error");
	}


	function getInput(id: string): HTMLInputElement {
		const result: HTMLElement = getElem(id);
		if (result instanceof HTMLInputElement)
			return result;
		throw new Error("Assertion error");
	}


	initialize();
}
