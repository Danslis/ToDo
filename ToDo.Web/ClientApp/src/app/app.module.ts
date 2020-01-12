import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HomeModule } from './home/home.module';
import { MatIconModule} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MyNavComponent } from './my-nav/my-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatListModule, MatGridListModule, MatCardModule, MatMenuModule } from '@angular/material';
import { AddDialogComponent } from './my-nav/add/add-dialog.component';
import { MaterialModule } from './material.module';
import { CardService } from './service/cardService';
import { CallService } from './service/callService';
import { SpinnerService } from './service/spinnerService';
import { SpinnerComponent } from './shared/spinner.component';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [
    AppComponent,
    MyNavComponent,
    AddDialogComponent,
    SpinnerComponent],
  
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    HomeModule,
    MatIconModule,
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    NgxSpinnerModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    ReactiveFormsModule,
    MaterialModule,
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }])
  ],
  providers: [CardService, CallService, SpinnerService],
  entryComponents: [
    AddDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
