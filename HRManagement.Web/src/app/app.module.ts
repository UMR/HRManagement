import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './common/components/header/header.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { HomeComponent } from './home/home.component';
import { NoticeComponent } from './notice/notice.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { NoticeService } from './notice/notice.service';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { UserInformationComponent } from './user-information/user-information.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
    NoticeComponent,
    PageNotFoundComponent,
    ForgotPasswordComponent,
    ChangePasswordComponent,
    UserInformationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [NoticeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
