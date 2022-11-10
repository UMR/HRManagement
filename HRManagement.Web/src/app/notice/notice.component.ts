import { Component, OnInit } from '@angular/core';
import { NoticeService } from './notice.service'

@Component({
  selector: 'app-notice',
  templateUrl: './notice.component.html',
  styleUrls: ['./notice.component.css']
})
export class NoticeComponent implements OnInit {

  topHeadingDisplay: any = [];

  constructor(private noticeService: NoticeService) { }

  ngOnInit(): void {
    this.noticeService.topHeading().subscribe((result) => {
      this.topHeadingDisplay = result.articles;
    })
  }

}
