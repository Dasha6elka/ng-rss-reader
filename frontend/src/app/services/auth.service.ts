import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly PATH = 'http://localhost:5000';

  constructor(private httpClient: HttpClient) {}

  private auth(username: string, password: string, path: string) {
    return this.httpClient
      .post<{ access_token: string; user_id: string }>(path, {
        username,
        password,
      })
      .pipe(
        tap((result) => {
          localStorage.setItem('access_token', result.access_token);
          localStorage.setItem('user_id', result.user_id);
        })
      );
  }

  login(username: string, password: string) {
    return this.auth(username, password, `${this.PATH}/api/users/token`);
  }

  register(username: string, password: string) {
    return this.auth(username, password, `${this.PATH}/api/users/register`);
  }

  isAuthed() {
    return (
      localStorage.getItem('access_token') && localStorage.getItem('user_id')
    );
  }
}
