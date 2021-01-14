import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly PATH = environment.apiUrl;

  loggedIn$ = new BehaviorSubject<boolean>(false);

  constructor(private httpClient: HttpClient) {
    if (this.isAuthed()) {
      this.loggedIn$.next(true);
    }
  }

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
          localStorage.setItem('user_name', username);

          this.loggedIn$.next(true);
        })
      );
  }

  login(username: string, password: string) {
    return this.auth(username, password, `${this.PATH}/api/users/token`);
  }

  register(username: string, password: string) {
    return this.auth(username, password, `${this.PATH}/api/users/register`);
  }

  updatePassword(password: string) {
    const userId = localStorage.getItem('user_id');
    const username = localStorage.getItem('user_name');

    return this.httpClient.put(
      `${this.PATH}/api/users/${userId}`,
      {
        idUser: parseInt(userId!),
        login: username,
        password,
      },
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('access_token')}`,
        },
      }
    );
  }

  isAuthed() {
    return localStorage.getItem('access_token') && localStorage.getItem('user_id');
  }

  logOut() {
    localStorage.clear();
    this.loggedIn$.next(false);
  }
}
