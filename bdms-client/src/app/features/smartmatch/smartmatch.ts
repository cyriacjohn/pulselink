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
  matchedDonors: any[] = [];
  constructor(private smartMatchingService: SmartMatchingService, private route: ActivatedRoute, private hospitalService: HospitalService) { }
  ngOnInit() {
/*    this.initMap();*/
    const requestId = Number(this.route.snapshot.paramMap.get('id'));
    console.log('Request ID:', requestId);
    this.findMatches(requestId);
  }

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
        [d.hospitalLatitude, d.hospitalLongtitude],
        [d.latitude, d.longitude]],
        {
          color: index < 3 ? 'green' : 'blue'
        }).addTo(this.map);
    });
  }

  findMatches(requestId: number) {
    this.smartMatchingService.smartMatch(requestId).subscribe((data: any) => {
      if (data.length > 0) {
        const hospitalLat = data[0].hospitalLatitude;
        const hospitalLng = data[0].hospitalLongtitude;
        this.map = L.map('map').setView([hospitalLat, hospitalLng], 12);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { attribution: '© OpenStreetMap' }).addTo(this.map);
        const hospitalIcon = L.icon({
          iconUrl: 'https://maps.google.com/mapfiles/ms/icons/red-dot.png',
          iconSize: [32, 32]
        });
        L.marker([hospitalLat, hospitalLng], {
          icon: hospitalIcon
        }).addTo(this.map).bindPopup("Hospital Location");
      }
      console.log(data);
      this.matchedDonors = data;
      this.plotDonors(data);
    });
  }
}
