import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import BaseService from '../baseService';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GenderService extends BaseService {

  constructor(private httpClient: HttpClient) {
    super(httpClient)
  }

  getAllGenders<T>(): Observable<T> {
    return this.get<T>(`gender/all`);
  }
}
