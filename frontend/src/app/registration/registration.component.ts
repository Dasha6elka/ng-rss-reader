import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {}

  ngOnInit() {}

  signUp() {
    const values = this.form.value;

    return this.authService.register(values.login, values.password).subscribe(
      () => {
        this.router.navigateByUrl('/');
        this.toastr.success('You have been successfully registered');
      },
      (error: HttpErrorResponse) => {
        const errorText = error.error as string;
        const statusText = error.statusText;
        if (errorText.includes('IX_user_account_login')) {
          this.toastr.error('This login already exists');
        } else {
          this.toastr.error(statusText);
        }
      }
    );
  }

  signIn() {
    this.router.navigateByUrl('/sign-in');
  }
}
