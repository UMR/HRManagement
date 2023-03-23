import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend, HttpResponse, HttpHeaders } from '@angular/common/http';
import { resourceServerUrl } from '../common/urls/registration';
import { Observable } from 'rxjs';
import { RegistrationModel } from '../common/models/registration';

@Injectable()
export class RegistrationService {
  private registrationURI: string = `${resourceServerUrl}/api/v1/Identity/Register`;

  constructor(private http: HttpClient, private httpBackend: HttpBackend) {
    this.http = new HttpClient(httpBackend);
  }

  registration(registrationModel: RegistrationModel) {
    return this.http.post(this.registrationURI, registrationModel);
  }
}
