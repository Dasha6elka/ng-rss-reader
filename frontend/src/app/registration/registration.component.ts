import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
  form = new FormGroup({
    login: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {}

  signUp() {
    const values = this.form.value;

    return this.authService.register(values.login, values.password).subscribe(() => {
      this.router.navigateByUrl('/');
    });
  }

  signIn() {
    this.router.navigateByUrl('/sign-in');
  }
}
