import {Component, ChangeDetectorRef} from '@angular/core';
import { News } from '../../core/services/news';
import { CommonModule } from '@angular/common';
import { DashboardService } from '../../core/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  news: any[] = [];
  stats: any;
  recentDonations: any[] = [];

  constructor(private newsService: News, private cdr: ChangeDetectorRef, private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.loadNews();
    this.loadStats();
    this.cdr.detectChanges();
  }

  loadNews() {
    this.newsService.getNews().subscribe(res => {
      this.news = res.articles?.slice(0, 10) ?? [];
    });
  }

  loadStats() {
    this.dashboardService.get().subscribe(res => {
      this.stats = res;
    })
  }
}
