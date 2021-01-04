import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ChannelService } from '../services/channel.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  form = new FormGroup({
    channel: new FormControl(''),
    link: new FormControl(''),
  });

  channels: Array<{
    id: number;
    channel: string;
    link: string;
    visible: boolean;
  }> = [];

  constructor(private channelService: ChannelService, private toastrService: ToastrService) {}

  ngOnInit(): void {
    this.channelService.getAll().subscribe((response) => {
      this.channels.push(...response);
    });
  }

  add() {
    var values = this.form.value;

    this.channelService.add(values.channel, values.link).subscribe((response) => {
      this.channels.push(response);
      this.toastrService.success(`"${values.channel}" has been successfully added`);
    }, this.handleHttpError);
  }

  clear() {
    this.form.reset();
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
