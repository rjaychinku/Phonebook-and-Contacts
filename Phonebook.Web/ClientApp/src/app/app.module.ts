import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EntryFormDialogComponent, PhonebookComponent } from './Phonebook/Phonebook.component';
import { AppAngMaterialModule } from './app-ang-material/app-ang-material.module';
import { OnlyNumberDirective } from './Directives/onlynumber.directive';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    OnlyNumberDirective,
    EntryFormDialogComponent,
    PhonebookComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    AppAngMaterialModule,
    RouterModule.forRoot([
      { path: '', component: PhonebookComponent, pathMatch: 'full' },
      { path: '**', redirectTo: '' }
    ], { relativeLinkResolution: 'legacy' }),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
