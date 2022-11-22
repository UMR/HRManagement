import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { HomeComponent } from './home/home.component';
import { HvncareersComponent } from './hvncareers/hvncareers.component';
import { LeaveApplicationComponent } from './leave-application/leave-application.component';
import { LoginComponent } from './login/login.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { RegistrationComponent } from './registration/registration.component';
import { UserInformationComponent } from './user-information/user-information.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'registration',
    component: RegistrationComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent
  }, 
  {
    path: 'change-password',
    component: ChangePasswordComponent
  },
  {
    path: 'user-information',
    component: UserInformationComponent
  }, 
  {
    path: 'leave-application',
    component: LeaveApplicationComponent
  },
  {
    path: 'page-not-found',
    component: PageNotFoundComponent
  }, 
  {
    path: 'hvncareers',
    component: HvncareersComponent
  },
  {
    path: '**',
    redirectTo: "/page-not-found"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
