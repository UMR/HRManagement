import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AssignRolesModel } from '../common/models/AssignRoles';
import { Role } from '../common/models/role';
import { User } from '../common/models/user';
import { AssignRoleService } from './assign-role.service';

@Component({
  selector: 'app-assign-role',
  templateUrl: './assign-role.component.html',
  styleUrls: ['./assign-role.component.css']
})
export class AssignRoleComponent implements OnInit {
  isLoading: boolean = true;
  selected = '';
  public roleAssignForm: any;
  public userList: User[] = [];
  public roleList: Role[] = [];
  public assignRolesModel: AssignRolesModel = {
    UserId: 0, RoleIds: []
  };

  constructor(private router: Router, private formBuilder: FormBuilder,
    private assignRoleService: AssignRoleService,
    private messageService: MessageService
  ) {
  }

  ngOnInit(): void {
    this.roleAssignForm = this.formBuilder.group({
      user: new FormControl(null, [Validators.required]),
      role: new FormControl(null, [Validators.required])
    });

    this.assignRoleService.getUsers()
      .subscribe(data => {
        this.userList = data as any;
      },
        err => {
          this.isLoading = false;
          this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Get users failed', detail: '' });
        },
        () => {
          this.isLoading = false;
        });


    this.assignRoleService.getRoles()
      .subscribe(data => {
        this.roleList = data as any;
      },
        err => {
          this.isLoading = false;
          this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Get roles failed', detail: '' });
        },
        () => {
          this.isLoading = false;
        });
  }

  onRadioChange(user: any) {
    this.assignRolesModel.UserId = user.id;
  }

  onCheckboxChange(role: any) {
    if (this.assignRolesModel.RoleIds.includes(role.id)) {
      const index = this.assignRolesModel.RoleIds.indexOf(role.id);
      if (index > -1) {
        this.assignRolesModel.RoleIds.splice(index, 1); 
      }
    }
    else {
      this.assignRolesModel.RoleIds.push(role.id);
    }
  }

  submitForm() {
    if (!this.assignRolesModel.UserId || this.assignRolesModel.UserId == 0) {
      this.messageService.add({ key: 'toastKey1', severity: 'warning', summary: 'Please select an user', detail: '' });
    }
    else if (this.assignRolesModel.RoleIds == [] || this.assignRolesModel.RoleIds.length == 0) {
      this.messageService.add({ key: 'toastKey1', severity: 'warning', summary: 'Please select at least one role', detail: '' });
    }
    else {
      this.assignRoleService.assignRolesToUser(this.assignRolesModel)
        .subscribe(data => {
          this.roleAssignForm.reset();
          this.messageService.add({ key: 'toastKey1', severity: 'success', summary: 'User role assign Successful', detail: '' });
        },
          err => {
            this.isLoading = false;
            this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'User role assign failed', detail: '' });
          },
          () => {
            this.isLoading = false;
          });
    }
  }
}
