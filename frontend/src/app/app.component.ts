import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  loggedIn$!: Observable<boolean>;

  constructor(private authService: AuthService) {
    this.loggedIn$ = this.authService.loggedIn$.asObservable();
  }
}
