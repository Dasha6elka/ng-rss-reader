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

  constructor(private channelService: ChannelService) {}

  ngOnInit(): void {}

  add() {
    var values = this.form.value;

    this.channelService.add(values.channel, values.link).subscribe();
  }
}
