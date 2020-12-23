import { AuthComponent } from "./auth/auth.component";
import { MainComponent } from "./main/main.component";
import { ProfileComponent } from "./profile/profile.component";
import { RegistrationComponent } from "./registration/registration.component";

export default [
  { path: 'sign-in', component: AuthComponent },
  { path: 'sign-up', component: RegistrationComponent },
  { path: 'profile', component: ProfileComponent },
  { path: '**', component: MainComponent }
]
