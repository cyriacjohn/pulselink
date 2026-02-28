import {Component, ChangeDetectorRef} from '@angular/core';
import { News } from '../../core/services/news';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  news: any[] = [];

  constructor(private newsService: News, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loadNews();
    this.cdr.detectChanges();
  }

  loadNews() {
    this.newsService.getNews().subscribe(res => {
      this.news = res.articles?.slice(0, 10) ?? [];
    });
  }
}
