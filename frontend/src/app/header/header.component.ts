import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  loggedIn$!: Observable<boolean>;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.loggedIn$ = this.authService.loggedIn$.asObservable();
  }

  logOut() {
    this.authService.logOut();
    this.router.navigateByUrl('/sign-in');
  }

}
