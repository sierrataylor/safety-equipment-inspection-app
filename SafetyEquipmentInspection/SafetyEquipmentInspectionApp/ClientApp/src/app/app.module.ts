import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
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
import { SharedService } from './shared.service';
import { FireExtinguisherFormComponent } from './inspection-form/fire-extinguisher-form/fire-extinguisher-form.component';
import { EquipmentTableComponent } from './equipment-table/equipment-table.component';
import { qrgeneratorComponent } from './qrgenerator/qrgenerator.component';
import { ToDoComponent} from './to-do/to-do.component';
import { SpillKitFormComponent } from './inspection-form/spill-kit-form/spill-kit-form.component';
import { EyewashFormComponent } from './inspection-form/eyewash-form/eyewash-form.component';
import { SafetyShowerFormComponent } from './inspection-form/safety-shower-form/safety-shower-form.component';
import { EyewashShowerFormComponent } from './inspection-form/eyewash-shower-form/eyewash-shower-form.component';
import { EmergencyLightingFormComponent } from './inspection-form/emergency-lighting-form/emergency-lighting-form.component';
import { FireBlanketsFormComponent } from './inspection-form/fire-blankets-form/fire-blankets-form.component';
import { FirstAidSuppliesFormComponent } from './inspection-form/first-aid-supplies-form/first-aid-supplies-form.component';
import { AedFormComponent } from './inspection-form/aed-form/aed-form.component';
import { FlammableCabinetsFormComponent } from './inspection-form/flammable-cabinets-form/flammable-cabinets-form.component';
import { LadderFormComponent } from './inspection-form/ladder-form/ladder-form.component';
import { HarnessFormComponent } from './inspection-form/harness-form/harness-form.component';
import { FpLanyardFormComponent } from './inspection-form/fp-lanyard-form/fp-lanyard-form.component';
import { HoistSlingFormComponent } from './inspection-form/hoist-sling-form/hoist-sling-form.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';


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
    PastInspectionsComponent,
    FireExtinguisherFormComponent,
    EquipmentTableComponent,
    qrgeneratorComponent,
    SpillKitFormComponent,
    EyewashFormComponent,
    SafetyShowerFormComponent,
    EyewashShowerFormComponent,
    EmergencyLightingFormComponent,
    FireBlanketsFormComponent,
    FirstAidSuppliesFormComponent,
    AedFormComponent,
    FlammableCabinetsFormComponent,
    LadderFormComponent,
    HarnessFormComponent,
    FpLanyardFormComponent,
    HoistSlingFormComponent,
    ToDoComponent,
    ForgotPasswordComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: '', component: LogInComponent, pathMatch: 'full' },
      { path: 'scanner', component: ScannerComponent },
      { path: 'inspection-form', component: InspectionFormComponent },
      { path: 'past-inspections', component: PastInspectionsComponent },
      { path: 'equipment-table', component: EquipmentTableComponent },
      { path: 'qrgenerator', component: qrgeneratorComponent },
      { path: 'to-do', component: ToDoComponent },
      { path: 'admin-dashboard', component: AdminDashboardComponent}
      { path: 'forgot-password', component: ForgotPasswordComponent }
    ])
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
