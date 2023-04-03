import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AccordionModule } from 'primeng/accordion'; 
import { FileUploadModule } from 'primeng/fileupload';

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
import { MessageService } from 'primeng/api';
import { MessagesModule } from 'primeng/messages';
import { ToastModule } from 'primeng/toast';
import { MessageModule } from 'primeng/message';
import { FileService } from './services/file.service';
import { LeaveApplicationComponent } from './leave-application/leave-application.component';
import { HvncareersComponent } from './hvncareers/hvncareers.component';
import { SendMailComponent } from './send-mail/send-mail.component';
import { RegistrationService } from './registration/registration.service';
import { LoginService } from './login/login.service';
import { TokenInterceptorService } from './common/token-interceptor.service';

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
    UserInformationComponent,
    LeaveApplicationComponent,
    HvncareersComponent,
    SendMailComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    DropdownModule,
    CalendarModule,
    AccordionModule,
    FileUploadModule,
    MessagesModule,
    MessageModule,
    ToastModule
  ],
  providers: [NoticeService,
    MessageService,
    FileService,
    RegistrationService,
    LoginService,
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
