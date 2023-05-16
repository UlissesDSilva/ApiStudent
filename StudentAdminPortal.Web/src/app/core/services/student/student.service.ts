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

  editStudent<T, Q>(id: string, body: Q): Observable<T> {
    return this.put<T>(`student/id/${id}`, body);
  }

  createStudent<T, Q>(body: Q): Observable<T>{
    return this.post<T>(`student/create`, body);
  }

  deleteStudent<T>(id: string): Observable<T> {
    return this.delete<T>(`student/id/${id}`);
  }

  uploadImageUrl<T>(id: string, file: File): Observable<any> {
    const formDate = new FormData();
    formDate.append("profileImage", file)
    return this.postMultipart<string>(`student/upload-image/${id}`, formDate)
  }

  getImagePath(relativePath: string) {
    return `${this.baseApiUrl}/${relativePath}`
  }
}
