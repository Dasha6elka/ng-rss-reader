import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ChannelService {
  private readonly PATH = 'http://localhost:5000';

  constructor(private httpClient: HttpClient) {}

  add(channel: string, link: string) {
    return this.httpClient.post<{
      id: number;
      channel: string;
      link: string;
    }>(
      `${this.PATH}/api/channels`,
      { channel, link },
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('access_token')}`,
        },
      }
    );
  }

  getAll() {
    return this.httpClient.get<
      Array<{
        id: number;
        channel: string;
        link: string;
      }>
    >(`${this.PATH}/api/channels`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('access_token')}`,
      },
    });
  }
}
