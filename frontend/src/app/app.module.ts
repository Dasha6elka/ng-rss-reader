import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { RegistrationComponent } from './registration/registration.component';
import { ProfileComponent } from './profile/profile.component';
import { MainComponent } from './main/main.component';

@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'sign-in', component: AuthComponent },
      { path: 'sign-up', component: RegistrationComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'main', component: MainComponent }
    ]),
    HttpClientModule,
  ],
  declarations: [
    AppComponent,
    RegistrationComponent,
    AuthComponent,
    ProfileComponent,
    MainComponent,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
