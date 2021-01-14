import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ChannelService {
  private readonly PATH = environment.apiUrl;

  constructor(private httpClient: HttpClient) {}

  add(channel: string, link: string) {
    return this.httpClient.post<{
      id: number;
      channel: string;
      link: string;
      visible: boolean;
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

  update(channel: { id: number; channel: string; link: string; visible: boolean }) {
    return this.httpClient.request('PUT', `${this.PATH}/api/channels/${channel.id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('access_token')}`,
      },
      body: channel,
    });
  }

  remove(id: number) {
    return this.httpClient.delete(`${this.PATH}/api/channels/${id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('access_token')}`,
      },
    });
  }

  getAll() {
    return this.httpClient.get<
      Array<{
        id: number;
        channel: string;
        link: string;
        visible: boolean;
      }>
    >(`${this.PATH}/api/channels`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('access_token')}`,
      },
    });
  }
}
