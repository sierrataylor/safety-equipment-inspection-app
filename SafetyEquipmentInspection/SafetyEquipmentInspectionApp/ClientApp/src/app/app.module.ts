import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LogInComponent } from './log-in/log-in.component';
import { ScannerComponent } from './scanner/scanner.component';
import { InspectionFormComponent } from './inspection-form/inspection-form.component';
import { InspectionsTableComponent } from './inspections-table/inspections-table.component';
import { PastInspectionsComponent } from './past-inspections/past-inspections.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    DashboardComponent,
    LogInComponent,
    ScannerComponent,
    InspectionFormComponent,
    InspectionsTableComponent,
    PastInspectionsComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: '', component: LogInComponent, pathMatch: 'full' },
      { path: 'scanner', component: ScannerComponent },
      { path: 'inspection-form', component: InspectionFormComponent },
      { path: 'past-inspections', component: PastInspectionsComponent },

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
