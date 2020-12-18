import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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

  channels: Array<{ id: number; channel: string; link: string }> = [];

  constructor(private channelService: ChannelService) {}

  ngOnInit(): void {
    this.channelService.getAll().subscribe((response) => {
      this.channels.push(...response);
    });
  }

  add() {
    var values = this.form.value;

    this.channelService
      .add(values.channel, values.link)
      .subscribe((response) => {
        this.channels.push(response);
      });
  }

  remove(id: number) {
    this.channelService.remove(id).subscribe(() => {
      this.channels = this.channels.filter((channel) => channel.id !== id);
    });
  }
}
