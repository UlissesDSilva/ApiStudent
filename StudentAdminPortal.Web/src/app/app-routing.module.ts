import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListStudentsComponent } from './layout/list-students/list-students.component';
import { CadStudentComponent } from './layout/cad-student/cad-student.component';

const routes: Routes = [
  {
    path: '',
    component: ListStudentsComponent
  },
  {
    path: 'cad-student',
    component: CadStudentComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
