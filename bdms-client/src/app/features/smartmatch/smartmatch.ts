import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { SmartMatchingService } from '../../core/services/smartmatching.service';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { HospitalService } from '../../core/services/hospital.service';
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-smartmatch',
  imports: [CommonModule],
  templateUrl: './smartmatch.html',
  styleUrl: './smartmatch.css',
})
export class SmartMatch implements OnInit {
  private map!: L.Map;
  constructor(private smartMatchingService: SmartMatchingService, private route: ActivatedRoute, private hospitalService: HospitalService) { }
  requests: any[] = [];
  ngOnInit() {
    this.hospitalService.getOpenRequests().subscribe((res: any) => {
      this.requests = res;
    });
    this.initMap();
  }

  initMap() {
    this.map = L.map('map').setView([10.0159, 76.3419], 12);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { attribution: '© OpenStreetMap' }).addTo(this.map);
    const hospitalIcon = L.icon({
      iconUrl: 'https://maps.google.com/mapfiles/ms/icons/red-dot.png',
      iconSize: [32, 32]
    });
    const hospitalLat = 10.030;
    const hospitalLng = 76.310;
    L.marker([hospitalLat, hospitalLng], {
      icon: hospitalIcon
    }).addTo(this.map).bindPopup("Hospital Location");
  }

  //loadDonors(requestId: number) {
  //  this.smartMatchingService.smartMatch(requestId).subscribe(data => {
  //    console.log(data);
  //    this.plotDonors(data);
  //  })
  //}

  plotDonors(donors: any[]) {
    donors.forEach((d, index) => {
      if (d.latitude == null || d.longitude == null) {
        return;
      }
      let iconUrl = 'https://maps.google.com/mapfiles/ms/icons/blue-dot.png';
      if (index < 3) {
        iconUrl = 'https://maps.google.com/mapfiles/ms/icons/green-dot.png';
      }

      const marker = L.marker([d.latitude, d.longitude], {
        icon: L.icon({ iconUrl: iconUrl, iconSize: [32, 32] })
      }).addTo(this.map);

      marker.bindPopup(`<b>${d.name} </b><br/>
                        Blood: ${d.bloodGroup} <br/>
                        Score: ${d.score}`);

      L.polyline([
        [d.hospitalLatitude, d.HospitalLongtitude],
        [d.latitude, d.longitude]],
        {
          color: index < 3 ? 'green' : 'blue'
        }).addTo(this.map);
    });
  }

  findMatches(requestId: number) {
    this.smartMatchingService.smartMatch(requestId).subscribe(data => {
      console.log(data);
      this.plotDonors(data);
    });
  }
}
