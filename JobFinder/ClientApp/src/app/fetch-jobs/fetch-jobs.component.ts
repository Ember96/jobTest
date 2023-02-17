import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Job } from '../models/job.model';

@Component({
  selector: 'app-fetch-job',
  templateUrl: './fetch-jobs.component.html'
})
export class FetchJobsComponent {
  public jobs: Job[] = [];
  panelOpenState = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Job[]>('http://localhost:5266/Jobs').subscribe(result => {
      this.jobs = result;
    }, error => console.error(error));
  }
}
