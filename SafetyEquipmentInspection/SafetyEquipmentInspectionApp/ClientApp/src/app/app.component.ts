import { Component } from '@angular/core';
import { SharedService } from './shared.service'
import { HttpClientModule } from '@angular/common/http'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
}

