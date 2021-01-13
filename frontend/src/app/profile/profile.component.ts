import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { ChannelService } from '../services/channel.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  private readonly PASSWORD_MIN_LENGTH = 8;
  private readonly PASSWORD_REG_EXP = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])');

  channelForm = new FormGroup({
    channel: new FormControl(''),
    link: new FormControl(''),
  });

  passwordForm = new FormGroup({
    newPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(this.PASSWORD_MIN_LENGTH),
      Validators.pattern(this.PASSWORD_REG_EXP),
    ]),
  });

  channels: Array<{
    id: number;
    channel: string;
    link: string;
    visible: boolean;
  }> = [];

  get password() {
    return new Proxy(this.passwordForm.controls.newPassword, {
      get(target, key: keyof AbstractControl) {
        if (key === 'errors') {
          const first = Object.keys(target.errors ?? [])?.[0];
          return { [first]: target.errors?.[first] };
        }
        return target[key];
      },
    });
  }

  get passwordErrorsMap(): Record<string, string> {
    return {
      required: 'Password is required',
      minlength: 'Password must be 8 characters or longer',
      pattern:
        'Password does not match pattern: must contain at least 1 lowercase alphabetical character, must contain at least 1 uppercase alphabetical character, must contain at least 1 numeric character',
    };
  }

  constructor(
    private channelService: ChannelService,
    private toastrService: ToastrService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.channelService.getAll().subscribe((response) => {
      this.channels.push(...response);
    });
  }

  add() {
    const values = this.channelForm.value;

    this.channelService.add(values.channel, values.link).subscribe((response) => {
      this.channels.push(response);
      this.toastrService.success(`"${values.channel}" has been successfully added`);
    }, this.handleHttpError);
  }

  clear() {
    this.channelForm.reset();
  }

  clearPassword() {
    this.passwordForm.reset();
  }

  update(id: number, event: MouseEvent) {
    const target = event.target as HTMLInputElement;
    const visible = target.checked;

    const channel = this.channels.find((channel) => channel.id === id);

    if (channel) {
      this.channelService.update({ ...channel, visible }).subscribe(() => {
        channel.visible = visible;
        this.toastrService.info(`"${channel.channel}" has been successfully updated`);
      }, this.handleHttpError);
    }
  }

  updatePassword() {
    const values = this.passwordForm.value;

    this.authService.updatePassword(values.newPassword).subscribe(() => {
      this.toastrService.info(`Password has been successfully updated`);
      this.clearPassword();
    }, this.handleHttpError);
  }

  remove(id: number) {
    const channel = this.channels.find((channel) => channel.id === id)!;

    this.channelService.remove(id).subscribe(() => {
      this.channels = this.channels.filter((channel) => channel.id !== id);
      this.toastrService.warning(`"${channel.channel}" has been successfully deleted`);
    }, this.handleHttpError);
  }

  private handleHttpError = (error: HttpErrorResponse) => {
    this.toastrService.error(error.statusText);
  };
}
