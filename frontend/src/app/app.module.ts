import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { RegistrationComponent } from './registration/registration.component';
import { ProfileComponent } from './profile/profile.component';
import { MainComponent } from './main/main.component';
import routes from './routes';
import { HeaderComponent } from './header/header.component';
import { FavoriteComponent } from './favorite/favorite.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
  ],
  declarations: [
    AppComponent,
    RegistrationComponent,
    AuthComponent,
    ProfileComponent,
    MainComponent,
    HeaderComponent,
    FavoriteComponent,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
