import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { InspectionDto } from '../SharedDTO/inspection.dto';
import { QuestionDto } from '../SharedDTO/question.dto';


@Component({
  selector: 'app-past-inspections',
  templateUrl: './past-inspections.component.html',
  styleUrls: ['./past-inspections.component.css']
})
export class PastInspectionsComponent implements OnInit {
  
  readonly APIUrl = "https://localhost:7023/";
  inspections: InspectionDto[] | any;
  questions: QuestionDto[] | any;

  constructor(public service: SharedService) { }

  ngOnInit(): void {
    this.GetInspectionsList();
    this.GetQuestionsList();
  }
  GetInspectionsList() {
    return this.service.GetInspectionsList().subscribe(data => {
      this.inspections = data;
    });
}
  GetQuestionsList(equipmentType: any = "") {
    return this.service.GetQuestionsList(equipmentType).subscribe(data => {
      this.questions = data;
    });
    }

  public openInfo()
  {
    const scan = document.getElementById('scaninfoId');
    if (scan != null) {
      scan.style.display = 'block';
    }
    
  }
  public closeInfo() {
    const scan = document.getElementById('scaninfoId');
    if (scan != null) {
      scan.style.display = 'block';
      
    }

  }
}
