import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent implements OnInit {
  form = new FormGroup({
    login: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  get login() {
    return this.form.controls.login;
  }

  get loginErrorsMap() {
    return {
      required: 'Login is required',
    };
  }

  get password() {
    return this.form.controls.password;
  }

  get passwordErrorsMap() {
    return {
      required: 'Password is required',
    };
  }

  constructor(private authService: AuthService, private router: Router, private toastrService: ToastrService) {}

  ngOnInit() {}

  signIn() {
    const values = this.form.value;

    this.authService.login(values.login, values.password).subscribe(
      () => {
        this.router.navigateByUrl('/');
      },
      (error: HttpErrorResponse) => {
        const errorText = error.error.errorText;
        const statusText = error.statusText;
        this.toastrService.error(errorText ?? statusText);
      }
    );
  }

  signUp() {
    this.router.navigateByUrl('/sign-up');
  }
}
