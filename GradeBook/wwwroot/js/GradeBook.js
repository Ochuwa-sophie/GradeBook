window.onload = function () {
    var grades = document.getElementsByClassName('grade');
    for (var i = 0; i < grades.length; i++) {
        var grade = grades[i];
        grade.onfocus = function () {
            this.style.border = '1px solid gray';
        };
        grade.onblur = function () {
            this.style.border = 'none';
        };
    }
};
function addAssignment(classID) {
    var title = document.getElementById('new-assignment-title').value;
    var date = document.getElementById('new-assignment-date').value;
    $.post('/gradebook/addassignment', {
        "title": title,
        "date": date,
        "classID": classID
    }, function () {
        location.reload();
    });
}
function updateScore(studentID, assignmentID, score, assessmentID) {
    if (assessmentID == null) {
        assessmentID = 0;
    }
    $.post('/GradeBook/UpdateScore', {
        "studentID": studentID,
        "assignmentID": assignmentID,
        "score": score,
        "assessmentID": assessmentID
    }, function () {
        location.reload();
    });
}
function updateAssignment(assignmentID, field, value) {
    $.post('/GradeBook/UpdateAssignment', {
        "assignmentID": assignmentID,
        "field": field,
        "value": value
    });
}
function sortGradeTable(column) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("GradeTable");
    switching = true;
    dir = "asc";
    while (switching) {
        switching = false;
        rows = table.getElementsByTagName("tr");
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            var row = rows[i];
            if (column == 0) {
                x = rows[i].innerText;
                y = rows[i + 1].innerText;
            }
            else {
                x = rows[i].cells[column]
                    .firstElementChild
                    .firstElementChild
                    .children[1].innerText;
                y = rows[i + 1].cells[column]
                    .firstElementChild
                    .firstElementChild
                    .children[1].innerText;
            }
            if (dir == "asc") {
                if (x > y) {
                    shouldSwitch = true;
                    break;
                }
            }
            else if (dir == "desc") {
                if (x < y) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        }
        else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}
function toggleStudentGrades(studentID) {
    var row = document.getElementById('student-row-' + studentID);
    var collapsed = row.getElementsByClassName('collapse');
    $(collapsed).collapse('toggle');
}
function renameStudent(studentID, name) {
    $.post('gradebook/renamestudent', {
        "studentID": studentID,
        "name": name
    }, function () {
        location.reload();
    });
}
