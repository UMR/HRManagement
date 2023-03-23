import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders, HttpBackend } from '@angular/common/http';
import { resourceServerUrl } from '../common/urls/auth-keys';
import { Observable } from 'rxjs';
import { LoginModel } from '../common/models/login';

@Injectable()
export class LoginService {
  private loginURI: string = `${resourceServerUrl}/api/v1/Identity/Login`;

  constructor(private http: HttpClient, private httpBackend: HttpBackend) {
    this.http = new HttpClient(httpBackend);
  }

  login(loginModel: LoginModel) {
    return this.http.post(this.loginURI, loginModel);
  }
}
