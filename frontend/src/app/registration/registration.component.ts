import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
  private readonly PASSWORD_MIN_LENGTH = 8;
  private readonly PASSWORD_REG_EXP = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])');

  form = new FormGroup({
    login: new FormControl('', [Validators.required]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(this.PASSWORD_MIN_LENGTH),
      Validators.pattern(this.PASSWORD_REG_EXP),
    ]),
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
    return new Proxy(this.form.controls.newPassword, {
      get(target, key) {
        if (key === 'errors') {
          const first = Object.keys(target.errors ?? [])?.[0];
          return { [first]: target.errors?.[first] };
        }
        return target.errors!;
      },
    });
  }

  get passwordErrorsMap() {
    return {
      required: 'Password is required',
      minlength: 'Password must be 8 characters or longer',
      pattern:
        'Password does not match pattern: must contain at least 1 lowercase alphabetical character, must contain at least 1 uppercase alphabetical character, must contain at least 1 numeric character',
    };
  }

  constructor(private authService: AuthService, private router: Router, private toastrService: ToastrService) {}

  ngOnInit() {}

  signUp() {
    const values = this.form.value;

    return this.authService.register(values.login, values.password).subscribe(
      () => {
        this.router.navigateByUrl('/');
        this.toastrService.success('You have been successfully registered');
      },
      (error: HttpErrorResponse) => {
        const errorText = error.error as string;
        const statusText = error.statusText;
        if (errorText.includes('IX_user_account_login')) {
          this.toastrService.error('This login already exists');
        } else {
          this.toastrService.error(statusText);
        }
      }
    );
  }

  signIn() {
    this.router.navigateByUrl('/sign-in');
  }
}
