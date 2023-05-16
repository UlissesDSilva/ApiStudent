import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "src/app/environments/environment";

export default class BaseService {
  protected readonly baseApiUrl = environment.baseApiUrl;

  protected constructor(private http: HttpClient) {
    this.http = http;
  }

  get<T>(path: string){
    return this.http.get<T>(`${this.baseApiUrl}/${path}`);
  }

  post<T>(path: string, body: any | null): Observable<T> {
    return this.http.post<T>(`${this.baseApiUrl}/${path}`, body, {headers: { 'Content-Type': 'application/json;' }});
  }
  postMultipart<T>(path: string, body: any | null): Observable<T> {
    let headers = new HttpHeaders({
        enctype: 'multipart/form-data',
        Accept: 'application/json'
    });
    return this.http.post<T>(`${this.baseApiUrl}/${path}`, body, { headers: headers });
}

  put<T>(path: string, body: any | null): Observable<T> {
    return this.http.put<T>(`${this.baseApiUrl}/${path}`, body, {headers: { 'Content-Type': 'application/json;' }});
  }

  delete<T>(path: string) {
    return this.http.delete<T>(`${this.baseApiUrl}/${path}`);
  }
}
