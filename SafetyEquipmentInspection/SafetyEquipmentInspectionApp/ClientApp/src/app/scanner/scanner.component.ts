import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-scanner',
  templateUrl: './scanner.component.html',
  styleUrls: ['./scanner.component.css']
})
export class ScannerComponent implements OnInit {

  constructor(private renderer2: Renderer2,
   @Inject(DOCUMENT) private _document: Document) { }

  ngOnInit(): void {
  }

}
