# flask_ngrok_example.py
from nltk.corpus import stopwords 
from nltk.tokenize import word_tokenize
from nltk.stem import PorterStemmer
from nltk.stem import WordNetLemmatizer
from flask import Flask,jsonify,request,make_response,url_for,redirect
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
import requests, json
import re
import joblib
import pickle

vectorizer = TfidfVectorizer()

app = Flask(__name__)

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

#This function will calulate the tf_idf of job description
def job_description_tfidf_vector(job_description):
    global vectorizer
    query_vector = []
    query_vector_res = []
    job_description = cleanResume(job_description)
    query_vector.append(job_description)
    query_vector_res = vectorizer.transform(query_vector)
    return  query_vector_res

@app.route("/",methods=['POST'])
def hello():
    resume = []
    data = request.json['resume']
    #Word Vectorizer object is been loaded which we saved in training.ipynb during training our model
    #We use the same object as we are using the trained here
    resume.append(data)
    resume[0] = cleanResume(resume[0])
    with open('tf_idf', 'rb') as tfidf:
            word_vectorizer = pickle.load(tfidf)
    WordFeatures = word_vectorizer.transform(resume)
    #Model which we trained in training.ipynb is being loaded to cateigries our corpus resumes
    trained_model = joblib.load('svm_trained_model')
    categories = trained_model.predict(WordFeatures)
    final_categories= dict(enumerate(categories.flatten(), 1))
    # print("cat"+categories)
    response = app.response_class(
            response=json.dumps(final_categories),
            status=200,
            mimetype='application/json'
        )
    return final_categories
@app.route("/JobSuggestion", methods = ['POST'])
def cosine_similarity_cal():
    global vectorizer
    resumes = request.json['resumes']
    job_description = request.json['job_description']
    
    cosine_answers = []
    X = vectorizer.fit(resumes)
    doc_vector = vectorizer.transform(resumes)
    query_vector_res = job_description_tfidf_vector(job_description[0])
    cosine_answers.append(cosine_similarity(doc_vector, query_vector_res))
    result = dict(enumerate(cosine_answers[0].flatten(), 1))
    return result
if __name__ == '__main__':
    app.run(host='127.0.0.1', port=80)  # If address is in use, may need to terminate other sessions:
               # Runtime > Manage Sessions > Terminate Other Sessions