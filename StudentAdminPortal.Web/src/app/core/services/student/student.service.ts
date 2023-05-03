import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import BaseService from '../baseService';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super(httpClient)
  }

  getAllStudents<T>(): Observable<T> {
    return this.get<T>('student/all');
  }

  getStudentByName<T>(name: string): Observable<T> {
    return this.get<T>(`student/${name}`);
  }

  getStudentById<T>(id: string): Observable<T>{
    return this.get<T>(`student/id/${id}`);
  }
}
