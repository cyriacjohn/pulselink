import {Component, ChangeDetectorRef} from '@angular/core';
import { News } from '../../core/services/news';
import { CommonModule } from '@angular/common';
import { DashboardService } from '../../core/services/dashboard.service';
import { NotificationService } from '../../core/services/notification.service';
import { Chart } from 'chart.js/auto';

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

  constructor(private newsService: News, private cdr: ChangeDetectorRef, private dashboardService: DashboardService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadNews();
    this.loadStats();
    this.createChart();
    this.notificationService.startConnection();
    this.notificationService.onDonationUpdate(() => {
      console.log("Live update received"); 
      this.loadStats();
    });
    this.cdr.detectChanges();
  }

  loadNews() {
    this.newsService.getNews().subscribe(res => {
      this.news = res.articles?.slice(0, 10) ?? [];
    });
  }

  loadStats() {
    this.dashboardService.get().subscribe(res => {
      this.stats = res.stats;
    })
  }

  createChart() {
    new Chart('donationChart', {
      type: 'bar',
      data: {
        labels: ["A+", "B+", "O+", "AB+"],
        datasets: [{
          label: "Blood Group Donations",
          data: [12, 19, 18, 4],
          backgroundColor: ["red", "blue", "green", "purple"]
        }]
      }
    })
  }
}
