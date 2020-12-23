import { Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { AuthGuard } from './guards/auth.guard';
import { MainComponent } from './main/main.component';
import { ProfileComponent } from './profile/profile.component';
import { RegistrationComponent } from './registration/registration.component';

const routes: Routes = [
  { path: 'sign-in', component: AuthComponent },
  { path: 'sign-up', component: RegistrationComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: '', component: MainComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' },
];

export default routes;
