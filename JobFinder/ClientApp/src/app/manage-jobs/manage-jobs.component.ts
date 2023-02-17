import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Job } from '../models/job.model';

@Component({
  selector: 'app-manage-job',
  templateUrl: './manage-jobs.component.html'
})
export class ManageJobsComponent {
  public jobs: Job[] = [];
  public job: Job = { jobId: 0, title: '', description: '', location: '', paymentPerHour: 0, company: '', contact: '', role: ''}
  public isUpdating = false;
  public currentJobID = -1;
  panelOpenState = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Job[]>('http://localhost:5266/Jobs').subscribe(result => {
      this.jobs = result;
    }, error => console.error(error));
  }

  public addJob( job: Job) {
    this.job.jobId = this.jobs.length + 1;
  this.jobs.push(this.job);
  this.http.post<Job>('http://localhost:5266/Jobs', this.job).subscribe(result => {
    this.job = result;
  });
  return this.job;
  }

  public deleteJob(job: Job) {
    this.http.delete<Job>('http://localhost:5266/Jobs/' + job.jobId).subscribe(result => {
      this.job = result;
    });
    this.jobs.splice(this.jobs.indexOf(job), 1);
  }

  public updateJob(job: Job) {
    if (this.currentJobID != -1) {
      job.jobId = this.currentJobID;
      this.http.put<Job>('http://localhost:5266/Jobs/' + job.jobId, job).subscribe(result => {
        this.job = result;
      });
    }
    this.isUpdating = false;
  }

  public toUpdate(id: number) {
    this.currentJobID = id;
    console.log(this.currentJobID)
    this.isUpdating = true;
  }
}
