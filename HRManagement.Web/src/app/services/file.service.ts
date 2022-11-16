import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor() { }

  private file: BehaviorSubject<string> = new BehaviorSubject<string>('');

  public getFile(): Observable<string> {
    return this.file.asObservable();
  }

  public setFile(value: string): void {

    this.file.next(value);
  }
}
