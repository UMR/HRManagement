import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  //public file: any;

  constructor() { }

 /* getImageFile(): Observable<string> {
    console.log('Service called');
    this.file = localStorage.getItem("fileData");
    this.file = this.file.toString().split('unsafe:SafeValue must use [property]=binding: ')[1];
    console.log(this.file);
    return this.file;
  }*/

  private file: BehaviorSubject<string> = new BehaviorSubject<string>('');

  public getFile(): Observable<string> {

    return this.file.asObservable();
  }

  public setFile(value: string): void {

    this.file.next(value);
  }
}
