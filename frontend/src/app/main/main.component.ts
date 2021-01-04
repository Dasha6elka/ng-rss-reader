import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { ChannelService } from '../services/channel.service';
import { FavoriteService } from '../services/favorite.service';

const CORS_PROXY = 'https://cors-anywhere.herokuapp.com/';

interface RSSItem {
  content: string;
  link: string;
  title: string;
}

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  selectedFeedId$ = new BehaviorSubject<number>(0);

  channels: Array<{ id: number; channel: string; link: string }> = [];

  feeds: Array<Parser.Output<RSSItem>> = [];

  parser = new RSSParser<object, RSSItem>();

  constructor(
    private channelService: ChannelService,
    private favoriteService: FavoriteService,
    private toastrService: ToastrService
  ) {}

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

  like(title: string, link: string) {
    this.favoriteService.add(title, link).subscribe(
      () => {
        this.toastrService.success(`"${title}" has been successfully favorited`);
      },
      (error: HttpErrorResponse) => {
        this.toastrService.error(error.statusText);
      }
    );
  }

  select(index: number) {
    this.selectedFeedId$.next(index);
  }
}
