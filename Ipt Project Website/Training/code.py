#important libraries that need to be imported
import pandas as pd
import re
from nltk.corpus import stopwords 
from nltk.tokenize import word_tokenize
from nltk.stem import PorterStemmer
from nltk.stem import WordNetLemmatizer
from sklearn.feature_extraction.text import TfidfVectorizer
import os
import sys
import joblib
import pickle
import pandas as pd
from PyQt5.uic import loadUi
from PyQt5.QtCore import QEvent, Qt
from PyQt5 import QtCore, QtGui, QtWidgets
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.feature_extraction.text import TfidfVectorizer
from PyQt5.QtWidgets import QDialog, QApplication, QFileDialog


#global variables
categories = []
resume = []
file_name = []
vectorizer = TfidfVectorizer()
doc_vector = []
query_vector = []
query_vector_res = []
cosine_answers = []
temp = {}
answers = []



#This function will pre-process the resume which are in the corpus and also Job Description
def cleanResume(resumeText):
      
    lemmatizer = WordNetLemmatizer()
    stemming = PorterStemmer()
    resumeText = resumeText.lower()
    resumeText = re.sub('http\S+\s*', ' ', resumeText)  # remove URLs
    resumeText = re.sub('RT|cc', ' ', resumeText)  # remove RT and cc
    resumeText = re.sub('#\S+', '', resumeText)  # remove hashtags
    resumeText = re.sub('@\S+', '  ', resumeText)  # remove mentions
    resumeText = re.sub('[%s]' % re.escape("""!"#$%&'()*+,-./:;<=>?@[\]^_`{|}~"""), ' ', resumeText)  # remove punctuations
    resumeText = re.sub(r'[^\x00-\x7f]',r' ', resumeText)
    resumeText = re.sub(r'[0-9]',' ', resumeText) 
    resumeText = re.sub('\s+', ' ', resumeText)  # remove extra whitespace
    word_tokens = word_tokenize(resumeText) 
    resumeText = [w for w in word_tokens if not w in stopwords.words("english")]
    resumeText = ' '.join(map(str, resumeText))
    resumeText = stemming.stem(resumeText)
    resumeText = lemmatizer.lemmatize(resumeText)
    return resumeText

#This function use the trained model which we trained in traing.ipynb
#The trained model help us to be categories the CV which are in the corpus
def resume_categorizer():
        global categories
        global resume
        global file_name
        global vectorizer
        global doc_vector

        # Resume Folder Path
        path = r"D:\University\Semester 6\IR\Rafay_Project\Project Code\Resume"

        # Change the directory
        os.chdir(path)

        # iterate through all file
        i=0
        for file in os.listdir():
            # Check whether file is in text format or not
            if file.endswith(".txt"):
                file_path = f"{path}\{file}"
                f_name = str(file)
                f_name = f_name.replace("D:/University/Semester 6/IR/Rafay_Project/Project Code/Resume/","")
                file_name.append(f_name)
                f = open(file_path, "r")
                resume.append(f.read())
                resume[i] = cleanResume(resume[i])
                i = i+1

        path = r"D:\University\Semester 6\IR\Rafay_Project\Project Code"
        os.chdir(path)

        #Word Vectorizer object is been loaded which we saved in training.ipynb during training our model
        #We use the same object as we are using the trained here
        with open('tf_idf', 'rb') as tfidf:
            word_vectorizer = pickle.load(tfidf)
        WordFeatures = word_vectorizer.transform(resume)
        #Model which we trained in training.ipynb is being loaded to cateigries our corpus resumes
        trained_model = joblib.load('svm_trained_model')
        categories = trained_model.predict(WordFeatures)
        sample_dic = {}
        for i in range(len(categories)):
            sample_dic.setdefault(categories[i], []).append(file_name[i])

        

        #compute document vector
        X = vectorizer.fit(resume)
        
        doc_vector = vectorizer.transform(resume)

        df1 = pd.DataFrame(doc_vector.toarray(),columns = vectorizer.get_feature_names())

        return sample_dic

#This function will calulate the tf_idf of job description
def job_description_tfidf_vector(job_description):

    global vectorizer
    global query_vector
    global query_vector_res

    cv = job_description
    cv = cleanResume(cv)
    if(len(query_vector)>0):
        query_vector.clear()
    query_vector.append(cv)
    query_vector_res = vectorizer.transform(query_vector)
    return  query_vector_res

#This function will calculate the cosine similarit between job description and resumes
def cosine_similarity_cal(query_vector_res):
    
    global cosine_answers
    global temp
    global answers
    res = {}
    if(len(cosine_answers)>0):
        cosine_answers.clear()
        answers.clear()
        temp.clear()
    cosine_answers.append(cosine_similarity(doc_vector, query_vector_res))

    for i in range(len(cosine_answers[0])):
        name = file_name[i]
        name = name.replace(".txt","")
        res[name] = cosine_answers[0][i]
            

    #sort the dictionary
    new_sort = sorted(res.items(), key=lambda x: x[1], reverse=True)
    if(len(new_sort)>10):
        top_ten_recommendation = new_sort[0:10]
    else:
        top_ten_recommendation = new_sort
    for i in top_ten_recommendation:
        answers.append(i[0])
   
    return answers






# Form implementation generated from reading ui file 'styling.ui'
#
# Created by: PyQt5 UI code generator 5.15.2
#
# WARNING: Any manual changes made to this file will be lost when pyuic5 is
# run again.  Do not edit this file unless you know what you are doing.




#class for graphical user interface
class Ui_MainWindow(QDialog):
    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(788, 600)
        MainWindow.setAutoFillBackground(False)
        self.centralwidget = QtWidgets.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")
        self.label = QtWidgets.QLabel(self.centralwidget)
        self.label.setGeometry(QtCore.QRect(0, 0, 211, 201))
        self.label.setTabletTracking(False)
        self.label.setWordWrap(True)
        self.label.setObjectName("label")
        self.label.setStyleSheet("background-image : url(image.jpg);background-position: center")
        self.frame = QtWidgets.QFrame(self.centralwidget)
        self.frame.setGeometry(QtCore.QRect(220, 30, 561, 271))
        self.frame.setFrameShape(QtWidgets.QFrame.StyledPanel)
        self.frame.setFrameShadow(QtWidgets.QFrame.Raised)
        self.frame.setObjectName("frame")
        self.frame_2 = QtWidgets.QFrame(self.frame)
        self.frame_2.setGeometry(QtCore.QRect(0, 10, 571, 41))
        self.frame_2.setFrameShape(QtWidgets.QFrame.StyledPanel)
        self.frame_2.setFrameShadow(QtWidgets.QFrame.Raised)
        self.frame_2.setObjectName("frame_2")
        self.label_2 = QtWidgets.QLabel(self.frame_2)
        self.label_2.setGeometry(QtCore.QRect(10, 10, 61, 16))
        self.label_2.setObjectName("label_2")
        self.lineEdit = QtWidgets.QLineEdit(self.frame_2)
        self.lineEdit.setGeometry(QtCore.QRect(80, 10, 321, 21))
        self.lineEdit.setObjectName("lineEdit")
        self.pushButton = QtWidgets.QPushButton(self.frame_2)
        self.pushButton.setGeometry(QtCore.QRect(444, 10, 101, 23))
        self.pushButton.setObjectName("pushButton")
        self.pushButton.clicked.connect(self.browsefiles)
        self.textEdit = QtWidgets.QTextEdit(self.frame)
        self.textEdit.setGeometry(QtCore.QRect(10, 60, 541, 171))
        self.textEdit.setObjectName("textEdit")
        self.pushButton_2 = QtWidgets.QPushButton(self.frame)
        self.pushButton_2.setGeometry(QtCore.QRect(450, 240, 101, 21))
        self.pushButton_2.setObjectName("pushButton_2")
        self.pushButton_2.clicked.connect(self.update)
        self.listWidget = QtWidgets.QListWidget(self.centralwidget)
        self.listWidget.setGeometry(QtCore.QRect(230, 330, 541, 191))
        self.listWidget.setObjectName("listWidget")
        self.label_3 = QtWidgets.QLabel(self.centralwidget)
        self.label_3.setGeometry(QtCore.QRect(20, 290, 171, 31))
        self.label_3.setObjectName("label_3")
        self.label_4 = QtWidgets.QLabel(self.centralwidget)
        self.label_4.setGeometry(QtCore.QRect(20, 520, 181, 21))
        self.label_4.setObjectName("label_4")
        self.label_5 = QtWidgets.QLabel(self.centralwidget)
        self.label_5.setGeometry(QtCore.QRect(220, 10, 511, 16))
        self.label_5.setObjectName("label_5")
        self.listWidget_2 = QtWidgets.QListWidget(self.centralwidget)
        self.listWidget_2.setGeometry(QtCore.QRect(20, 330, 181, 192))
        self.listWidget_2.setObjectName("listWidget_2")
        print("RESUMES PRE-PROCESSING IN PROGRESS.............")
        categorized_resume = resume_categorizer()
        print("RESUME PRE-PROCESSING DONE !!!!!")
        print("PROGRAM STARTED !!!")
        for key in categorized_resume:
            self.listWidget_2.addItem("Cateogry :"+str(key)+"\n")
            for i in range(len(categorized_resume[key])):
                self.listWidget_2.addItem("Documet ====>"+str(categorized_resume[key][i]))
            self.listWidget_2.addItem("\n")
        self.listWidget_2.installEventFilter(self)
        MainWindow.setCentralWidget(self.centralwidget)
        self.menubar = QtWidgets.QMenuBar(MainWindow)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 788, 21))
        self.menubar.setObjectName("menubar")
        MainWindow.setMenuBar(self.menubar)
        self.statusbar = QtWidgets.QStatusBar(MainWindow)
        self.statusbar.setObjectName("statusbar")
        MainWindow.setStatusBar(self.statusbar)
        self.retranslateUi(MainWindow)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

    def update(self):
        text = self.textEdit.toPlainText()
        jd_result = job_description_tfidf_vector(text)
        result = cosine_similarity_cal(jd_result)
        self.listWidget.clear()
        self.listWidget.addItem("=======Ranking========")
        for i in range(0,len(result)):
            self.listWidget.addItem("Rank: "+str(i+1)+"  =====>  "+"Document " + str(result[i]))
        
    def eventFilter(self, source, event):
        import subprocess
        if event.type() == QEvent.ContextMenu and source is self.listWidget_2:
            x = self.listWidget_2.currentItem().text()
            x = x.replace("Documet ====>","")
            x = x.replace(".txt","")
            path = "D:/University/Semester 6/IR/Rafay_Project/Project Code/original_resume/"+str(x)+".pdf"
            subprocess.Popen([path], shell=True)
            return True

        return super().eventFilter(source, event)


    def browsefiles(self):
        fname = QFileDialog.getOpenFileName(self, 'Open file', 'D:/University/Semester 6/IR/Rafay_Project/Project Code/Job Description')
        name = fname[0].replace("D:/University/Semester 6/IR/Rafay_Project/Project Code/Job Description/","")
        if(len(name)>0):
            content = open(fname[0]).read()
            self.lineEdit.setText(name)
            self.textEdit.setPlainText(content)


    def retranslateUi(self, MainWindow):
        _translate = QtCore.QCoreApplication.translate
        MainWindow.setWindowTitle(_translate("MainWindow", "MainWindow"))
        self.label.setText(_translate("MainWindow", "<html><head/><body><p align=\"center\"><span style=\" font-size:36pt; font-weight:600; vertical-align:sub;\">Automated Resume Suggester</span></p></body></html>"))
        self.label_2.setText(_translate("MainWindow", "<html><head/><body><p><span style=\" font-size:10pt;\">File Name</span></p></body></html>"))
        self.pushButton.setText(_translate("MainWindow", "Browse"))
        self.pushButton_2.setText(_translate("MainWindow", "Suggest"))
        self.label_3.setText(_translate("MainWindow", "<html><head/><body><p><span style=\" font-size:9pt; font-weight:600;\">All Categorized Resume</span></p></body></html>"))
        self.label_4.setText(_translate("MainWindow", "<html><head/><body><p><span style=\" font-size:7pt; font-weight:600; color:#ff0000;\">* Right click the resume to open it</span></p></body></html>"))
        self.label_5.setText(_translate("MainWindow", "<html><head/><body><p><span style=\" font-size:7pt; font-weight:600; color:#ff0000;\">* Select Job Description file by pressing browse or write in the text panel below, then press suggest</span></p></body></html>"))

#This from where program will start
if __name__ == "__main__":
    import sys
    app = QtWidgets.QApplication(sys.argv)
    MainWindow = QtWidgets.QMainWindow()
    ui = Ui_MainWindow()
    ui.setupUi(MainWindow)
    MainWindow.show()
    sys.exit(app.exec_())
