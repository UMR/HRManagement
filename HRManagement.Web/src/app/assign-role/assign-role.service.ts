import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AssignRolesModel } from '../common/models/AssignRoles';
import { resourceServerUrl } from '../common/urls/auth-keys';

@Injectable()
export class AssignRoleService {
  private getUsersURI: string = `${resourceServerUrl}/api/v1/Users/GetUsers`;
  private getRolesURI: string = `${resourceServerUrl}/api/v1/Roles/GetRoles`;
  private assignRolesURI: string = `${resourceServerUrl}/api/v1/Roles/AssignRolesToUser`;
  
  constructor(private http: HttpClient) {    
  }

  getUsers() {
    return this.http.get(this.getUsersURI);
  }

  getRoles() {
    return this.http.get(this.getRolesURI);
  }

  assignRolesToUser(assignRolesModel: AssignRolesModel) {
    return this.http.post(this.assignRolesURI, assignRolesModel);
  }
}
