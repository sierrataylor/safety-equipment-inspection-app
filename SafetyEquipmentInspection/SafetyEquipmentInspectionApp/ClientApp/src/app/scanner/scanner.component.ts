import { Component, OnInit } from '@angular/core';
import { Renderer2, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
declare function initalizescanner(): any;
declare function submit(): any;
@Component({
  selector: 'app-scanner',
  templateUrl: './scanner.component.html',
  styleUrls: ['./scanner.component.css']
})
export class ScannerComponent implements OnInit {

  constructor(private renderer2: Renderer2,
   @Inject(DOCUMENT) private _document: Document) { }

  ngOnInit() {
 
    const s = this.renderer2.createElement('script');
    s.type = 'text/javascript';
    s.src = 'https://rawgit.com/sitepoint-editors/jsqrcode/master/src/qr_packed.js';
    this.renderer2.appendChild(this._document.body, s);
  }

}
