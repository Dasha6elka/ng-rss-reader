import { Component, OnInit } from '@angular/core';
import { ChannelService } from '../services/channel.service';

const CORS_PROXY = 'https://cors-anywhere.herokuapp.com/';

interface RSSItem {
  content: string;
  title: string;
  link: string;
}

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  channels: Array<{ id: number; channel: string; link: string }> = [];

  feeds: Array<Parser.Output<RSSItem>> = [];

  parser = new RSSParser<object, RSSItem>();

  constructor(private channelService: ChannelService) {}

  ngOnInit(): void {
    this.channelService.getAll().subscribe(async (response) => {
      this.channels.push(...response);

      for (const channel of response) {
        try {
          const feed = await this.parser.parseURL(CORS_PROXY + channel.link);
          this.feeds.push(feed);
        } catch (error) {
          console.error(error);
        }
      }
    });
  }
}
