import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { RegistrationComponent } from './registration/registration.component';

@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'sign-in', component: AuthComponent },
      { path: 'sign-up', component: RegistrationComponent },
    ]),
    HttpClientModule,
  ],
  declarations: [AppComponent, RegistrationComponent, AuthComponent],
  bootstrap: [AppComponent],
})
export class AppModule {}
