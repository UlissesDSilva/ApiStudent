import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

export default class BaseService {
  private readonly baseApiUrl = "http://localhost:5018";

  protected constructor(private http: HttpClient) {
    this.http = http;
  }

  get<T>(path: string){
    return this.http.get<T>(`${this.baseApiUrl}/${path}`);
  }

  post<T>(path: string, body: any | null): Observable<T> {
    return this.http.post<T>(`${this.baseApiUrl}/${path}`, body, {headers: { 'Content-Type': 'application/json;' }});
  }

  put<T>(path: string, body: any | null): Observable<T> {
    return this.http.put<T>(`${this.baseApiUrl}/${path}`, body, {headers: { 'Content-Type': 'application/json;' }});
  }

  delete<T>(path: string) {
    return this.http.delete<T>(`${this.baseApiUrl}/${path}`);
  }
}
