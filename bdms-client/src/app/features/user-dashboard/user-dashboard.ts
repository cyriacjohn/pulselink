import { Component, ChangeDetectorRef } from '@angular/core';
import { News } from '../../core/services/news';
import { CommonModule } from '@angular/common';
import { DashboardService } from '../../core/services/dashboard.service';
import { NotificationService } from '../../core/services/notification.service';
import { Chart } from 'chart.js/auto';

@Component({
  selector: 'app-user-dashboard',
  imports: [CommonModule],
  templateUrl: './user-dashboard.html',
  styleUrl: './user-dashboard.css',
})
export class UserDashboard {
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
    this.dashboardService.getBloodGroupStats().subscribe((res: any) => {
      const labels = Object.keys(res);
      const values = Object.values(res);
      new Chart('donationChart', {
        type: 'bar',
        data: {
          labels: labels,
          datasets: [{
            label: "Blood Group Donations",
            data: values,
            backgroundColor: ["red", "blue", "green", "purple"]
          }]
        }
      })
    })

  }
}
