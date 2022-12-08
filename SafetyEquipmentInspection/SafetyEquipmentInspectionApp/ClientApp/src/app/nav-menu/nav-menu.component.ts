import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public static showAdministrativeSettings: boolean | undefined = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  showAdministrativeSettings() {
    console.log(NavMenuComponent.showAdministrativeSettings);
    return NavMenuComponent.showAdministrativeSettings;
  }
}
