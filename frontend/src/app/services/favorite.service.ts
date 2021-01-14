import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FavoriteService {
  private readonly PATH = environment.apiUrl;

  constructor(private httpClient: HttpClient) {}

  add(title: string, link: string) {
    return this.httpClient.post<{
      id: number;
      title: string;
      link: string;
    }>(
      `${this.PATH}/api/favorites`,
      { title, link },
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
        title: string;
        link: string;
      }>
    >(`${this.PATH}/api/favorites`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('access_token')}`,
      },
    });
  }

  remove(id: number) {
    return this.httpClient.delete(`${this.PATH}/api/favorites/${id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('access_token')}`,
      },
    });
  }
}
