import { environment } from './../../environments/environment';

import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { User } from '../models/User';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getUser(id): Observable<User>{
  return this.http.get<User>(this.baseUrl + 'users/' + id);
}
getUsers(): Observable<User>{
  return this.http.get<User>(this.baseUrl + 'users');
}

}
